const { projectPath } = require("./constant");

const jsDist = projectPath.relativeOutputPath + "/js";
const styleDist = projectPath.relativeOutputPath + "/style";

const clientRoot = projectPath.clientInputPath;

module.exports = {
    [`${jsDist}/pages/commission/commission-group`]: clientRoot + "/js/pages/commission-group/index.jsx",
    [`${jsDist}/pages/commission/assign-group`]: clientRoot + "/js/pages/commission-group/components/assign-group/index.jsx",
    [`${jsDist}/pages/commission/assign-private-group`]: clientRoot + "/js/pages/commission-group/components/assign-private-group/index.jsx",
    [`${jsDist}/pages/Recheckin/recheckin`]: clientRoot + "/js/pages/RecheckIn/recheckin",
    [`${jsDist}/pages/layout/pagelayout`]: clientRoot + "/js/pagelayout/pagelayout.js",
    [`${jsDist}/pages/report/affiliate-report`]: clientRoot + "/js/pages/affiliate-report/index.jsx",
    [`${jsDist}/pages/userpanel/daily-earning`]: clientRoot + "/js/pages/user-panel/daily-earning/index.jsx",
    [`${jsDist}/pages/userpanel/daily-earning-detail`]: clientRoot + "/js/pages/user-panel/daily-earning-detail/index.jsx",
    [`${jsDist}/pages/userpanel/player-detail`]: clientRoot + "/js/pages/user-panel/player-detail/index.jsx",
    [`${jsDist}/pages/media-gallery/landing-page`]: clientRoot + "/js/pages/media-gallery/landing-page/index.jsx",

    [`${styleDist}/portalmain`]: clientRoot + "/style/main.scss",
    [`${styleDist}/layout`]: clientRoot + "/style/layout/main-layout.scss",
    [`${styleDist}/user-panel-layout`]: clientRoot + "/style/layout/user-panel-layout.scss",
    [`${styleDist}/pages/commission-group`]: clientRoot + "/style/pages/commission-group.scss",
    [`${styleDist}/pages/affiliate-report`]: clientRoot + "/style/pages/affiliate-report.scss",
    [`${styleDist}/pages/user-panel/player-detail`]: clientRoot + "/style/pages/user-panel/player-detail.scss",
    [`${styleDist}/pages/media-gallery/landing-page`]: clientRoot + "/style/pages/media-gallery/landing-page.scss"
};