import React, { Fragment } from 'react';
import { connect } from 'react-redux';
import NoteContent from './NoteContent';
import ModifyNote from './ModifyNote';

export const AppContainer = (props) => {

    return (
        <Fragment>
            <NoteContent />
            <ModifyNote />
        </Fragment>
    );
}


export default connect(() => ({}), () => ({}))(React.memo(AppContainer));