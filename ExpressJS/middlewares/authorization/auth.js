const CustomError = require("../../helpers/error/CustomError");
const jwt = require("jsonwebtoken");
const { isTokenIncluded, getAccessTokenFromHeader } = require("../../helpers/authorization/tokenHelpers");

const getAccessToRoute = (req, res, next) => {

    const { JWT_SECRET_KEY } = process.env;
    if (!isTokenIncluded(req)) {
        /// 401 Unauthorized giriş yapmadan erişmeye çalışmak
        /// 403 Forbidden giriş yapsan bile adminin gireceği yere girmeye çalışmak
        return next(new CustomError("Headers wrong or empty.", 401));
    }
    const accessToken = getAccessTokenFromHeader(req);

    jwt.verify(accessToken, JWT_SECRET_KEY, (err, decoded) => {
        if (err) {
            return next(new CustomError("Auth token wrong or time is finished.", 401));
        }
        req.user = { /// User is a variable can be anything and accessable from same route funcs. Example: req.user.id
            id: decoded.id,
            name: decoded.name,
        }
        next();
    })
}

module.exports = {
    getAccessToRoute
}