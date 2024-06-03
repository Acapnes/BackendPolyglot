const accessControl = (req, res, next) => {
  res.status(200).json({
    success: true,
    message: "You are uthorized",
    status: 200
  });
  next(); /// Next Yazılmassa request kullanıcı tarafından gönderilir serverdan yanıt gelmez
};

module.exports = {
  accessControl,
};
