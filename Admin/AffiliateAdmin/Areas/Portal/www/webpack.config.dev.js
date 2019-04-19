const merge = require("webpack-merge");

const { environment } = require("./config-utils/constant");
const config = require("./webpack.config");

const { projectPath } = require("./config-utils/constant");

const jsDist = projectPath.relativeOutputPath + "/js";
const styleDist = projectPath.relativeOutputPath + "/style";

const clientRoot = projectPath.clientInputPath;

const devConfig = config(environment.development)[0];

devConfig.entry = {
    [`${jsDist}/pages/customer/mainpage`]: clientRoot + "/js/pages/customer/index.jsx",
    [`${styleDist}/pages/customer/leftpanel`]: clientRoot + "/style/pages/customer/leftpanel.scss"
}

console.log(devConfig.entry);

module.exports = devConfig;