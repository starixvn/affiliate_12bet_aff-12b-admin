const merge = require("webpack-merge");

const { environment } = require("./config-utils/constant");
const config = require("./webpack.config");

const { projectPath } = require("./config-utils/constant");

const jsDist = projectPath.relativeOutputPath + "/js";
const styleDist = projectPath.relativeOutputPath + "/style";

const clientRoot = projectPath.clientInputPath;

const devConfig = config(environment.development)[0];

devConfig.entry = {
    [`${jsDist}/pages/userpanel/user-account`]: clientRoot + "/js/pages/user-panel/user-account/index.jsx",
    [`${styleDist}/pages/user-panel/user-account`]: clientRoot + "/style/pages/user-panel/user-account.scss"
}

console.log(devConfig.entry);

module.exports = devConfig;