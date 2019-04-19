import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Content as LeftPanelContent } from '../../left-panel/components/Content';
import { setMonthDate, toggleLoader, setIframeUrl } from '../../left-panel/actions';

//import { Content as CustomerStatisticsContent } from '../../customer-statistics/components/Content';

const leftPanelRedux = {
    mapStateToProps: ({ leftPanel }) => {
        const { affiliateInfo, affName, monthDate, iframeUrl } = leftPanel;
        return Object.assign({}, { affiliateInfo, affName, monthDate, iframeUrl });
    },
    mapDispatchToProps: (dispatch) => bindActionCreators({ setMonthDate, toggleLoader, setIframeUrl }, dispatch)
}

export const LeftPanel = connect(leftPanelRedux.mapStateToProps, leftPanelRedux.mapDispatchToProps)(LeftPanelContent);