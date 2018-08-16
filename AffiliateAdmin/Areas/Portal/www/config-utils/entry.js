const path = require("path");
const { projectPath } = require("./constant");

const jsDist = "js";
const styleDist = "style";

module.exports = {
    [`${jsDist}/commission-group`]: "./source/js/pages/commission-group/index.jsx",
    [`${jsDist}/assign-normal-group`]: "./source/js/pages/commission-group/components/assign-normal-group/index.jsx",
    [`${jsDist}/assign-private-group`]: "./source/js/pages/commission-group/components/assign-private-group/index.jsx",
    //[`${jsDist}/affiliate-information`]: "./source/js/pages/affiliate-information/index.jsx",
    [`${jsDist}/pages/Recheckin/recheckin`]: "./source/js/pages/RecheckIn/recheckin",

    [`${styleDist}/portalmain`]: "./source/style/main.scss",
    [`${styleDist}/layout`]: "./source/style/layout/main-layout.scss",
    [`${styleDist}/commission-group`]: "./source/style/pages/commission-group.scss",
    [`${styleDist}/affiliate-information`]: "./source/style/pages/affiliate-information.scss"
};