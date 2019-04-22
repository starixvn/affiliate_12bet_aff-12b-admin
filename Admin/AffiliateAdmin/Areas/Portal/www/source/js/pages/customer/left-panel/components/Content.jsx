import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import SearchBox from './SearchBox';
import MainInfo from './MainInfo';
import { setMonthDate, toggleLoader, setIframeUrl } from '../actions';

export class Content extends PureComponent {
    constructor(props) {
        super(props);

        this.state = {
            isChange: true
        }
    }

    handleSetUrl = (url) => {
        this.setState({ isChange: !this.state.isChange });
        this.props.setIframeUrl(url);
    }

    render() {
        return (
            <div className="left-panel border m-2 p-2">
                <SearchBox />
                {this.props.affiliateInfo && <MainInfo />}
            </div>
        );
    }
}

function mapStateToProps({ leftPanel }) {
    const { affiliateInfo, affName, monthDate, iframeUrl } = leftPanel;
    return Object.assign({}, { affiliateInfo, affName, monthDate, iframeUrl });
};

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setMonthDate, toggleLoader, setIframeUrl }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);