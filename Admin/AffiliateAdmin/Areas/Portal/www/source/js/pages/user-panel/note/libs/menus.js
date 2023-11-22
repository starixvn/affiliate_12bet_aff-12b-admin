import React from 'react'
import { Claim, claimResource } from '../../../../modules/claim';
import { menuKey } from '../libs/constants';

const AffNote = React.lazy(() => import( /* webpackChunkName: "affNote" */ '../aff-note/components/AppContainer'));
const MarketingNote = React.lazy(() => import(/* webpackChunkName: "marketingNote" */ '../marketing-note/components/AppContainer'));

export const menus = [
    { key: menuKey.affNote, label: 'Note', component: AffNote, renderClaim: new Claim(claimResource.Note) },
    { key: menuKey.marketingNote, label: 'Marketing Note', component: MarketingNote, renderClaim: new Claim(claimResource.MarketingTeamNote) }
]