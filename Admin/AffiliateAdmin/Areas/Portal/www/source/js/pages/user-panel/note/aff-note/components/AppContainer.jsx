import React from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';

import AppLayout from '../../../../../layouts/AppLayout';
import NoteContent from './NoteContent';
import ModifyNote from './ModifyNote';
import UserPanelSubMenu from '../../../../common/components/UserPanelSubMenu';

import { setSelectedUserPanel } from '../actions';

export const AppContainer = ({ setSelectedUserPanel }) => {

    const subMenuList = [
        { isActive: true, handleClick: () => setSelectedUserPanel('note'), title: 'Note' },
        { isActive: false, handleClick: () => setSelectedUserPanel('marketingNote'), title: 'Marketing Team Note' }
    ];

    return (
        <AppLayout>
            <UserPanelSubMenu subMenuList={subMenuList} />
            <NoteContent />
            <ModifyNote />
        </AppLayout>
    );
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setSelectedUserPanel }, dispatch);
}

export default connect(() => ({}), mapDispatchToProps)(React.memo(AppContainer));