const path = require("path");
const { projectPath } = require("./constant");

const jsDist = "js";
const styleDist = "style";

module.exports = {
    [`${jsDist}/pages/commission/commission-group`]: "./source/js/pages/commission-group/index.jsx",
    [`${jsDist}/pages/commission/assign-group`]: "./source/js/pages/commission-group/components/assign-group/index.jsx",
    [`${jsDist}/pages/commission/assign-private-group`]: "./source/js/pages/commission-group/components/assign-private-group/index.jsx",
    [`${jsDist}/pages/Recheckin/recheckin`]: "./source/js/pages/RecheckIn/recheckin",

    [`${styleDist}/portalmain`]: "./source/style/main.scss",
    [`${styleDist}/layout`]: "./source/style/layout/main-layout.scss",
    [`${styleDist}/pages/commission-group`]: "./source/style/pages/commission-group.scss"
};