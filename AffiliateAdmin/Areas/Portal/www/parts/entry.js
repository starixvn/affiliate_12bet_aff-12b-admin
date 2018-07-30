const path = require("path");
const { projectPath } = require("./constant");

const { clientInputPath } = projectPath;

const jsDist = "js";
const styleDist = "style";

module.exports = {
    [`${jsDist}/commission`]: "./source/js/pages/commission/index.jsx",
    [`${jsDist}/affiliate-information`]: "./source/js/pages/affiliate-information/index.jsx",

    [`${styleDist}/bootstrap`]: "./source/style/vendors/_bootstrap.scss",
    [`${styleDist}/portalmain`]: "./source/style/main.scss",
    [`${styleDist}/layout`]: "./source/style/layout/main-layout.scss",
    [`${styleDist}/commission`]: "./source/style/pages/commission.scss",
    [`${styleDist}/affiliate-information`]: "./source/style/pages/affiliate-information.scss"
};