const { use } = require("express/lib/application");

const sendJwtToClient = (user, res) => {
    try {
      const token = user.generateJwtFromUser();
  
      const { JWT_COOKIE, NODE_ENV } = process.env;
  
      const cookieOptions = {
        httpOnly: true,
        expires: new Date(Date.now() + parseInt(JWT_COOKIE) * 1000 * 60),
        secure: NODE_ENV === "production" // Set secure flag based on environment
      };
  
      // Send response with token and cookie
      return res.status(200).cookie("access_token", token, cookieOptions).json({
        success: true,
        access_token: token,
        data: {
          name: user.name,
          email: user.email
        }
      });
    } catch (error) {
      // Handle errors
      console.error("Error while sending JWT:", error);
      return res.status(500).json({ success: false, message: "Internal Server Error" });
    }
  };
  

const isTokenIncluded = req => {
    return (req.headers.authorization && req.headers.authorization.startsWith('Bearer:'));
}

const getAccessTokenFromHeader = (req) => {
    const authorization = req.headers.authorization;
    const access_token = authorization.split(" ")[1];
    return access_token;
}

module.exports = {
    sendJwtToClient,
    isTokenIncluded,
    getAccessTokenFromHeader
};