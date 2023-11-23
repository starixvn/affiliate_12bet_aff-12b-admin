import React, { Fragment } from 'react';

import MarketingNoteContent from './MarketingNoteContent';
import ModifyMarketingNote from './ModifyMarketingNote';

export const AppContainer = () => {
    return (
        <Fragment>
            <MarketingNoteContent />
            <ModifyMarketingNote />
        </Fragment>
    );
}

export default React.memo(AppContainer);