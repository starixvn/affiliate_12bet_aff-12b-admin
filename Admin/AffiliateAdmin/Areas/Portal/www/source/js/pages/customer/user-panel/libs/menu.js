import React from 'react';
import { Claim, claimResource } from '../../../../modules/claim';
import { userPanelMenuKey as menuKey } from '../../common/libs/constants';
import { withUserPanel } from './withUserPanel';

const StatementHistory = React.lazy(() =>
    import( /* webpackChunkName: "statementHistory" */ '../../../user-panel/statement-history/components/AppContainer'));
const DailyEarning = React.lazy(() =>
    import( /* webpackChunkName: "dailyEarning" */ '../../../user-panel/daily-earning/components/AppContainer'));
const DailyEarningDetail = React.lazy(() =>
    import( /* webpackChunkName: "dailyEarningDetail" */ '../../../user-panel/daily-earning-detail/components/AppContainer'));
const UserAccount = React.lazy(() =>
    import( /* webpackChunkName: "userAccount" */ '../../../user-panel/user-account/components/AppContainer'));
const Note = React.lazy(() =>
    import( /* webpackChunkName: "note" */ '../../../user-panel/note/index'));
const PlayerDetail = React.lazy(() =>
    import( /* webpackChunkName: "playerDetail" */ '../../../user-panel/player-detail/components/AppContainer'));
const Adjustment = React.lazy(() =>
    import( /* webpackChunkName: "adjustment" */ '../../../user-panel/adjustment/components/AppContainer'));
const Member = React.lazy(() =>
    import( /* webpackChunkName: "member" */ '../../../user-panel/customer-member/components/AppContainer'));
const ViewLog = React.lazy(() =>
    import( /* webpackChunkName: "viewLog" */ '../../../user-panel/view-log/components/AppContainer'));
const ReferrerStatistics = React.lazy(() =>
    import( /* webpackChunkName: "referrerStatistics" */ '../../../user-panel/referrers/components/AppContainer'));
const LandingPageStatistics = React.lazy(() =>
    import( /* webpackChunkName: "landingPageStatistics" */ '../../../user-panel/landing-page-statistics/components/AppContainer'));

export const userPanelMenuList = [
    { key: menuKey.statementHistory, name: 'Statement History', component: withUserPanel(StatementHistory, menuKey.statementHistory), parent: null },
    { key: menuKey.dailyEarning, name: '', component: withUserPanel(DailyEarning, menuKey.dailyEarning), parent: menuKey.statementHistory },
    { key: menuKey.dailyEarningDetail, name: '', component: withUserPanel(DailyEarningDetail, menuKey.dailyEarningDetail), parent: menuKey.statementHistory },
    { key: menuKey.edit, name: 'Edit', component: withUserPanel(UserAccount, menuKey.edit), parent: null, renderClaim: new Claim(claimResource.EditAffiliate) },
    { key: menuKey.note, name: 'Note', component: withUserPanel(Note, menuKey.note), parent: null, renderClaim: [new Claim(claimResource.Note), new Claim(claimResource.MarketingTeamNote)] },
    { key: menuKey.playerDetail, name: 'Players Detail', component: withUserPanel(PlayerDetail, menuKey.playerDetail), parent: null, renderClaim: new Claim(claimResource.AffPlayerDetail) },
    { key: menuKey.adjustment, name: 'Adjustment', component: withUserPanel(Adjustment, menuKey.adjustment), parent: null, renderClaim: new Claim(claimResource.AffAdjustment, claimResource.WritePermission) },
    { key: menuKey.member, name: 'Member', component: withUserPanel(Member, menuKey.member), parent: null, renderClaim: new Claim(claimResource.MemberDetail) },
    { key: menuKey.viewLog, name: 'View Log (Old)', component: withUserPanel(ViewLog, menuKey.viewLog), parent: null, renderClaim: new Claim(claimResource.ViewLog) },
    { key: menuKey.clickStatistics, name: 'Click Statistics', component: withUserPanel(ReferrerStatistics, menuKey.clickStatistics), parent: null },
    { key: menuKey.landingPageStatistics, name: '', component: withUserPanel(LandingPageStatistics, menuKey.landingPageStatistics), parent: menuKey.clickStatistics },
]

export const userPanelPendingMenuList = [
    { key: menuKey.edit, name: 'Edit', component: withUserPanel(UserAccount, menuKey.edit), parent: null, renderClaim: new Claim(claimResource.EditAffiliate) },
    { key: menuKey.note, name: 'Note', component: withUserPanel(Note, menuKey.note), parent: null, renderClaim: [new Claim(claimResource.Note), new Claim(claimResource.MarketingTeamNote)] },
]