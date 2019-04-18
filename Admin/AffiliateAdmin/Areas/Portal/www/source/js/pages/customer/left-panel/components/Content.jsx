import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Row, Col } from 'reactstrap';

import SearchBox from './SearchBox';
import MainInfo from './MainInfo';
import { setMonthDate, toggleLoader, setIframeUrl } from '../actions';

import { Content as CustomerStatistics } from '../../customer-statistics/components/Content';

export class Content extends Component {
    constructor(props) {
        super(props);

        this.state={
            isChange: true
        }
    }

    handleSetUrl = (url) => {
        this.setState({isChange: !this.state.isChange});
        this.props.setIframeUrl(url);
    }

    _renderRightPanel() {
        const { affiliateInfo, affName, monthDate, iframeUrl } = this.props;

        return  affiliateInfo && (
            <React.Fragment>
                <CustomerStatistics
                    monthDate={monthDate}
                    affId={affiliateInfo.AffId}
                    affName={affiliateInfo.AffName}
                    handleSetUrl={this.handleSetUrl}
                    toggleLoader={this.props.toggleLoader}
                    setMonthDate={this.props.setMonthDate}
                />
                <iframe id="user-panel" src={`${iframeUrl}&${this.state.isChange}`}></iframe>
            </React.Fragment>
        );
    }

    render() {
        return (
            <div className='data-content m-2'>
                <Row>
                    <div className="left-panel border m-2 p-2">
                            <SearchBox />
                            {this.props.affiliateInfo && <MainInfo />}
                    </div>
                    <div className="right-panel mx-2">
                        {this._renderRightPanel()}
                    </div>
                </Row>
            </div>
        );
    }
}

function mapStateToProps({ affiliateInfo, affName, monthDate, iframeUrl }) {
    return Object.assign({}, { affiliateInfo, affName, monthDate, iframeUrl });
};

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setMonthDate, toggleLoader, setIframeUrl }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);