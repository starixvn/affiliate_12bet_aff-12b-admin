import React from 'react';
import { userPanelMenuKey as menuKey } from '../../common/modules/constants';

const StatementHistory = React.lazy(() =>
    import ( /* webpackChunkName: "statementHistory" */ '../../../user-panel/statement-history/components/AppContainer'));
const DailyEarning = React.lazy(() =>
    import ( /* webpackChunkName: "dailyEarning" */ '../../../user-panel/daily-earning/components/AppContainer'));
const DailyEarningDetail = React.lazy(() =>
    import ( /* webpackChunkName: "dailyEarningDetail" */ '../../../user-panel/daily-earning-detail/components/AppContainer'));
const UserAccount = React.lazy(() =>
    import ( /* webpackChunkName: "userAccount" */ '../../../user-panel/user-account/components/AppContainer'));
const Note = React.lazy(() =>
    import ( /* webpackChunkName: "note" */ '../../../user-panel/note/components/AppContainer'));
const PlayerDetail = React.lazy(() =>
    import ( /* webpackChunkName: "playerDetail" */ '../../../user-panel/player-detail/components/AppContainer'));
const Adjustment = React.lazy(() =>
    import ( /* webpackChunkName: "adjustment" */ '../../../user-panel/adjustment/components/AppContainer'));
const TrackingStatistics = React.lazy(() =>
    import ( /* webpackChunkName: "trackingStatistics" */ '../../../user-panel/tracking-statistics/components/AppContainer'));
const Member = React.lazy(() =>
    import ( /* webpackChunkName: "member" */ '../../../user-panel/customer-member/components/AppContainer'));
const ViewLog = React.lazy(() =>
    import ( /* webpackChunkName: "viewLog" */ '../../../user-panel/view-log/components/AppContainer'));
const ReferrerStatistics = React.lazy(() =>
    import ( /* webpackChunkName: "referrerStatistics" */ '../../../user-panel/referrers/components/AppContainer'));
const LandingPageStatistics = React.lazy(() =>
    import ( /* webpackChunkName: "landingPageStatistics" */ '../../../user-panel/landing-page-statistics/components/AppContainer'));

export const userPanelMenuList = [
    { key: menuKey.statementHistory, name: 'Statement History', component: StatementHistory, parent: null },
    { key: menuKey.dailyEarning, name: '', component: DailyEarning, parent: menuKey.statementHistory },
    { key: menuKey.dailyEarningDetail, name: '', component: DailyEarningDetail, parent: menuKey.statementHistory },
    { key: menuKey.edit, name: 'Edit', component: UserAccount, parent: null },
    { key: menuKey.note, name: 'Note', component: Note, parent: null },
    { key: menuKey.playerDetail, name: 'Players Detail', component: PlayerDetail, parent: null },
    { key: menuKey.adjustment, name: 'Adjustment', component: Adjustment, parent: null },
    { key: menuKey.trackingStatistics, name: 'Tracking Statistics', component: TrackingStatistics, parent: null },
    { key: menuKey.member, name: 'Member', component: Member, parent: null },
    { key: menuKey.viewLog, name: 'View Log', component: ViewLog, parent: null },
    { key: menuKey.clickStatistics, name: 'Click Statistics', component: ReferrerStatistics, parent: null },
    { key: menuKey.landingPageStatistics, name: '', component: LandingPageStatistics, parent: menuKey.clickStatistics }
]

export const userPanelPendingMenuList = [
    { key: menuKey.edit, name: 'Edit', component: UserAccount, parent: null },
    { key: menuKey.note, name: 'Note', component: Note, parent: null }
]