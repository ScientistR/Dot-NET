    public async Task<CommonResponseModel> loginUser(LoginModel login)
        {
            CommonResponseModel response = new();
            try
            {

                if (!string.IsNullOrEmpty(login.UserName) && !string.IsNullOrEmpty(login.Password))
                {
                    

                    var user = await _userManager.FindByNameAsync(login.UserName).ConfigureAwait(false);
                    var Data = await _userManager.CheckPasswordAsync(user, login.Password).ConfigureAwait(false);
                    if(Data)
                    {
                        //var zipverify = _dbContext.ZipCodes.Where(zip => zip.Zip == user.ZipCode).ToList();
                        //if(zipverify.Count>0)
                        //{
                        var UserRole = _dbContext.Roles.Where(res => res.Id == user.Role).FirstOrDefault();
                        var UserDeatils = new RegistrationModel();
                        var loginResponseModel = new LoginResponseModel();
                        var loginResponseToken = new LoginResponseToken();
                        loginResponseModel.UserId = user.Id;
                        loginResponseModel.EmailAddress = user.UserName;
                        loginResponseModel.FirstName = user.FirstName;
                        loginResponseModel.LastName = user.LastName;
                        loginResponseModel.Address = user.Address;
                        loginResponseModel.Address1 = user.Address1;
                        loginResponseModel.MobileNo = user.MobileNo;
                        loginResponseModel.City = user.City;
                        loginResponseModel.ZipCode = user.ZipCode;
                        loginResponseModel.Country = user.Country;
                        loginResponseModel.RoleId = user.Role;
                        loginResponseModel.RoleName = UserRole.Name;
                        loginResponseModel.ReferralCode = user.ReferralCode;
                        loginResponseModel.IsNotification = user.IsNotification;
                        loginResponseModel.Token = GenerateJSONWebToken(login, user.Role);

                        //UserToken userToken = new UserToken();
                        //userToken.DeviceId = login.DeviceId;
                        //userToken.DeviceToken = login.DeviceToken;
                        //userToken.UserId = user.Id;
                        //userToken.ModifyBy = user.Id;
                        //userToken.CreatedBy = user.FirstName+ " "+user.LastName;
                        //userToken.CreatedDate = loginResponseToken.CreatedDate = DateTime.UtcNow.Date;
                        //userToken.ModifyDate = loginResponseToken.ModifyDate= DateTime.UtcNow.Date;
                        //_dbContext.Update(userToken);
                        //_dbContext.SaveChanges();
                        var DeviceInfo = _dbContext.UserToken.Where(a => a.UserId == user.Id && a.DeviceId == login.DeviceId).FirstOrDefault();
                        if (DeviceInfo != null)
                        {
                            DeviceInfo.DeviceId = login.DeviceId;
                            DeviceInfo.UserId = user.Id;
                            DeviceInfo.DeviceToken = login.DeviceToken;
                            DeviceInfo.CreatedBy = user.FirstName;
                            DeviceInfo.CreatedDate = DateTime.UtcNow;
                            _dbContext.Update(DeviceInfo);
                            _dbContext.SaveChanges();
                        }
                        else
                        {
                            UserToken userToken = new UserToken();
                            userToken.DeviceId = login.DeviceId;
                            userToken.UserId = user.Id;
                            userToken.DeviceToken = login.DeviceToken;
                            userToken.CreatedBy = user.FirstName;
                            userToken.CreatedDate = DateTime.UtcNow;
                            _dbContext.UserToken.Add(userToken);
                            _dbContext.SaveChanges();
                        }
                        response.Data = loginResponseModel;
                        response.Message = TKMessages.login;
                        response.StatusCode = (int)HttpStatusCode.OK;


                        //}
                        //else
                        //{
                        //    response.Data = null;
                        //    response.Message = TKMessages.ZipMessage;
                        //    response.StatusCode = (int)HttpStatusCode.BadRequest;
                        //}

                    }

                    else
                    {
                        response.Data = null;
                        response.Message = TKMessages.invaliduser;
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                    }
                }
                else
                {
                    response.Data = null;
                    response.Message = TKMessages.failed;
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.Data = null;
                response.Message = TKMessages.failed;
                response.StatusCode = (int)HttpStatusCode.BadGateway;
            }
            return response;
        }
        /// <summary>
        /// GenerateJSONWebToken
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        private string GenerateJSONWebToken(LoginModel login,string roleId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, login.UserName.ToString()),
                //new Claim(JwtRegisteredClaimNames., userInfo.Password),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("Role",roleId)
            };

            var token = new JwtSecurityToken(Config["Jwt:Issuer"],
                Config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddDays(365),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// OtpResetPassword
        /// </summary>
        /// <param name="OtpResetPassword"></param>
        /// <returns></returns>
        public async Task<APIResponseModel> OtpSend(SendOtpModel sendOtp, EmailSettings emailSettings)
        {
            UtilityFunction function = new();
            APIResponseModel response = new();
            LoginResponse retdata = new();
            // int status = 0;
            try
            {
                if (!string.IsNullOrEmpty(sendOtp.UserName))
                {
                    var userDetails = await _dbContext.Users.Where(x => x.UserName == sendOtp.UserName).FirstOrDefaultAsync().ConfigureAwait(true);
                    if (userDetails != null)
                    {
                        var otpDetails = GenerateRandomOTP();
                        /// save otp from database
                        userDetails.OTP = otpDetails;
                        userDetails.OTPCreatedDate = DateTime.UtcNow;
                        _dbContext.Update(userDetails);
                        var result = await _dbContext.SaveChangesAsync();
                        EmailInfo emailInfo = new();
                        emailInfo.EmailTo = userDetails.UserName;
                        emailInfo.Subject = TKMessages.subject;  // "Test";
                        //emailInfo.Body = "Hi This is your otp" + otpDetails;
                        emailInfo.Body = TKMessages.otpbodymessage + otpDetails;
                        function.SendEmailAsync(emailInfo, emailSettings);
                        return response = function.DataResponse(retdata, true, TKMessages.OTP, (int)HttpStatusCode.OK);
                    }
                    else
                    {
                        return response = function.DataResponse(retdata, true, TKMessages.invalidemail, (int)HttpStatusCode.BadRequest);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return response = function.DataResponse(retdata, true, TKMessages.failed, (int)HttpStatusCode.BadRequest);
        }

        /// <summary>
        /// GenerateRandomOTP
        /// </summary>
        /// <returns></returns>
        public string GenerateRandomOTP()
        {
            string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
            string sOTP = String.Empty;
            string sTempChars = String.Empty;
            Random rand = new();
            for (int i = 0; i < 4; i++)
            {
                // int p = rand.Next(0, saAllowedCharacters.Length);

                sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                sOTP += sTempChars;
            }
            return sOTP;
        }

        /// <summary>
        /// GenrateReferralRandomCode
        /// </summary>
        /// <returns></returns>
        public string GenrateReferralRandomCode()
        {
            char[] chars = "abcdefghijklmnopqrstuvwxyz1234567890".ToCharArray();
            StringBuilder sb = new(); //StringBuilder
            Random random = new SecureRandom();
            for (int i = 0; i < 6; i++)
            {
                char c = chars[random.Next(chars.Length)];
                sb.Append(c);
            }
            String output = sb.ToString();
            return output;
        }

        /// <summary>
        ///  add customer order by admin
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        ///
