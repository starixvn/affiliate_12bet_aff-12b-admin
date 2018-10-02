const path = require("path");
const { projectPath } = require("./constant");

const jsDist = "js";
const styleDist = "style";

module.exports = {
    [`${jsDist}/pages/commission/commission-group`]: "./source/js/pages/commission-group/index.jsx",
    [`${jsDist}/pages/commission/assign-group`]: "./source/js/pages/commission-group/components/assign-group/index.jsx",
    [`${jsDist}/pages/commission/assign-private-group`]: "./source/js/pages/commission-group/components/assign-private-group/index.jsx",
    [`${jsDist}/pages/Recheckin/recheckin`]: "./source/js/pages/RecheckIn/recheckin",
    [`${jsDist}/pages/layout/pagelayout`]: "./source/js/pagelayout/pagelayout.js",
    [`${jsDist}/pages/report/affiliate-report`]: "./source/js/pages/affiliate-report/index.jsx",
    [`${jsDist}/pages/userpanel/daily-earning`]: "./source/js/pages/user-panel/daily-earning/index.jsx",
    [`${jsDist}/pages/userpanel/daily-earning-detail`]: "./source/js/pages/user-panel/daily-earning-detail/index.jsx",
    [`${jsDist}/pages/userpanel/player-detail`]: "./source/js/pages/user-panel/player-detail/index.jsx",
    [`${jsDist}/pages/marketing/landing-page`]: "./source/js/pages/marketing/landing-page/index.jsx",

    [`${styleDist}/portalmain`]: "./source/style/main.scss",
    [`${styleDist}/layout`]: "./source/style/layout/main-layout.scss",
    [`${styleDist}/user-panel-layout`]: "./source/style/layout/user-panel-layout.scss",
    [`${styleDist}/pages/commission-group`]: "./source/style/pages/commission-group.scss",
    [`${styleDist}/pages/affiliate-report`]: "./source/style/pages/affiliate-report.scss",
    [`${styleDist}/pages/user-panel/player-detail`]: "./source/style/pages/user-panel/player-detail.scss",
    [`${styleDist}/pages/marketing/landing-page`]: "./source/style/pages/marketing/landing-page.scss"
};