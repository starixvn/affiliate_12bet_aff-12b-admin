import React, { Fragment } from 'react';
import AffNoteContent from './AffNoteContent';
import ModifyAffNote from './ModifyAffNote';

export const AppContainer = () => {

    return (
        <Fragment>
            <AffNoteContent />
            <ModifyAffNote />
        </Fragment>
    );
}


export default React.memo(AppContainer);