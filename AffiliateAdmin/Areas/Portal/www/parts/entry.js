const path = require("path");
const { projectPath } = require("./constant");

const { clientInputPath } = projectPath;

const jsDist = "js";
const styleDist = "style";

module.exports = {
    [`${jsDist}/sample`]: "./source/js/pages/sample/components/index.jsx",

    [`${styleDist}/bootstrap`]: "./source/style/vendors/_bootstrap.scss",
    [`${styleDist}/sample`]: "./source/style/pages/_sample.scss"
};
