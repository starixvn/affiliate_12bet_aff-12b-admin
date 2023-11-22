import React, { Fragment } from 'react';
import { connect } from 'react-redux';

import MarketingNoteContent from './MarketingNoteContent';
import ModifyMarketingNote from './ModifyMarketingNote';

export const AppContainer = (props) => {
    return (
        <Fragment>
            <MarketingNoteContent />
            <ModifyMarketingNote />
        </Fragment>
    );
}

export default connect(() => ({}), () => ({}))(React.memo(AppContainer));