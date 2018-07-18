const path = require("path");
const { projectPath } = require("./constant");

const { clientInputPath } = projectPath;

const jsDist = "js";
const styleDist = "style";

module.exports = {
    [`${jsDist}/sample`]: "./source/js/pages/sample/components/index.jsx",
    [`${jsDist}/commission`]: "./source/js/pages/commission/index.jsx",
    [`${jsDist}/affiliate-information`]: "./source/js/pages/affiliate-information/index.jsx",

    [`${styleDist}/bootstrap`]: "./source/style/vendors/_bootstrap.scss",
    [`${styleDist}/portalmain`]: "./source/style/main.scss",
    [`${styleDist}/commission`]: "./source/style/pages/commission.scss",
    [`${styleDist}/sample`]: "./source/style/pages/_sample.scss"
};