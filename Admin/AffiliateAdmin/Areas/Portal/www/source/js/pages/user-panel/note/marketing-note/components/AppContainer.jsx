import React from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';

import AppLayout from '../../../../../layouts/AppLayout';
import MarketingNoteContent from './MarketingNoteContent';
import ModifyMarketingNote from './ModifyMarketingNote';
import UserPanelSubMenu from '../../../../common/components/UserPanelSubMenu';

import { setSelectedUserPanel } from '../actions';

export const AppContainer = ({ setSelectedUserPanel }) => {

    const subMenuList = [
        { isActive: false, handleClick: () => setSelectedUserPanel('note'), title: 'Note' },
        { isActive: true, handleClick: () => setSelectedUserPanel('marketingNote'), title: 'Marketing Team Note' }
    ];

    return (
        <AppLayout>
            <UserPanelSubMenu subMenuList={subMenuList} />
            <MarketingNoteContent />
            <ModifyMarketingNote />
        </AppLayout>
    );
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setSelectedUserPanel }, dispatch);
}

export default connect(() => ({}), mapDispatchToProps)(React.memo(AppContainer));