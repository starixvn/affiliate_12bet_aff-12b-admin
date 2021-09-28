const { projectPath } = require("./constant");

const jsDist = projectPath.relativeOutputPath + "/js";
const styleDist = projectPath.relativeOutputPath + "/style";

const clientRoot = projectPath.clientInputPath;

module.exports = {
    //Home
    [`${jsDist}/pages/home`]: clientRoot + "/js/pages/home/index.jsx",

    [`${styleDist}/home`]: clientRoot + "/style/pages/home.scss",

    //Customer
    [`${jsDist}/pages/customer/customer`]: clientRoot + "/js/pages/customer/index.jsx",

    [`${styleDist}/pages/customer`]: clientRoot + "/style/pages/customer/main.scss",
    [`${styleDist}/user-panel-layout`]: clientRoot + "/style/layout/user-panel-layout.scss",

    //Affiliate
    [`${jsDist}/pages/affiliate/affiliate`]: clientRoot + "/js/pages/affiliate/index.jsx",
    [`${jsDist}/pages/commission/assign-group`]: clientRoot + "/js/pages/affiliate/commission-group/components/assign-group/index.jsx",
    [`${jsDist}/pages/commission/assign-private-group`]: clientRoot + "/js/pages/affiliate/commission-group/components/assign-private-group/index.jsx",


    [`${styleDist}/pages/affiliate`]: clientRoot + "/style/pages/affiliate/affiliate.scss",
    [`${styleDist}/pages/commission-group`]: clientRoot + "/style/pages/affiliate/commission-group.scss",
    

    //Member
    [`${jsDist}/pages/member/member`]: clientRoot + "/js/pages/member/index.jsx",

    [`${styleDist}/pages/member/active-member`]: clientRoot + "/style/pages/member/active-member.scss",

    //Report
    [`${jsDist}/pages/report/report`]: clientRoot + "/js/pages/report/index.jsx",

    [`${styleDist}/pages/report`]: clientRoot + "/style/pages/report.scss",

    //Media Gallery
    [`${jsDist}/pages/media-gallery/media-gallery`]: clientRoot + "/js/pages/media-gallery/index.jsx",

    [`${styleDist}/pages/media-gallery/landing-page`]: clientRoot + "/style/pages/media-gallery/landing-page.scss",
    [`${styleDist}/pages/media-gallery/text-list`]: clientRoot + "/style/pages/media-gallery/text-list.scss",
    [`${styleDist}/pages/media-gallery/banner-list`]: clientRoot + "/style/pages/media-gallery/banner-list.scss",
    [`${styleDist}/pages/media-gallery/news-letter`]: clientRoot + "/style/pages/media-gallery/news-letter.scss",

    //Message
    [`${jsDist}/pages/message/message`]: clientRoot + "/js/pages/message/index.jsx",

    [`${styleDist}/pages/message/message`]: clientRoot + "/style/pages/message/message.scss",

    //Exchange Rate
    [`${jsDist}/pages/exchange-rate/exchange-rate`]: clientRoot + "/js/pages/exchange-rate/index.jsx",

    [`${styleDist}/pages/exchange-rate/exchange-rate`]: clientRoot + "/style/pages/exchange-rate/exchange-rate.scss",

    //Admin Role
    [`${jsDist}/pages/admin-role/admin-list`]: clientRoot + "/js/pages/admin-role/admin-list/index.jsx",
    [`${jsDist}/pages/admin-role/role-permission`]: clientRoot + "/js/pages/admin-role/role-permission/index.jsx",

    [`${styleDist}/pages/admin-role/admin-list`]: clientRoot + "/style/pages/admin-role/admin-list.scss",
    [`${styleDist}/pages/admin-role/role-permission`]: clientRoot + "/style/pages/admin-role/role-permission.scss",

    //Domain
    [`${jsDist}/pages/domain/domain`]: clientRoot + "/js/pages/domain/domain/index.jsx",
    [`${jsDist}/pages/domain/domain-control`]: clientRoot + "/js/pages/domain/domain-control/index.jsx",

    [`${styleDist}/pages/domain/domain`]: clientRoot + "/style/pages/domain/domain.scss",
    [`${styleDist}/pages/domain/domain-control`]: clientRoot + "/style/pages/domain/domain-control.scss",

    //Common
    [`${jsDist}/pages/login`]: clientRoot + "/js/pages/login/index.jsx",
    [`${jsDist}/pages/under-maintenance`]: clientRoot + "/js/pages/under-maintenance/index.jsx",
    [`${jsDist}/pages/change-admin-password`]: clientRoot + "/js/pages/change-admin-password/index.jsx",


    [`${styleDist}/portalmain`]: clientRoot + "/style/main.scss",
    [`${styleDist}/layout`]: clientRoot + "/style/layout/main-layout.scss",
    [`${styleDist}/login`]: clientRoot + "/style/pages/login.scss",
    [`${styleDist}/under-maintenance`]: clientRoot + "/style/pages/under-maintenance.scss",
    [`${styleDist}/change-admin-password`]: clientRoot + "/style/pages/change-admin-password.scss",
};