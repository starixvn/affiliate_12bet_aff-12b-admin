import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import SearchBox from './SearchBox';
import MainInfo from './MainInfo';
import { toggleLoader } from '../actions';

const Content = (props) => {
    return (
        <div className="left-panel-container p-2">
            <SearchBox />
            {props.affiliateInfo && <MainInfo />}
        </div>
    )
}

function mapStateToProps({ leftPanel }) {
    const { affiliateInfo } = leftPanel;
    return Object.assign({}, { affiliateInfo });
};

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ toggleLoader }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);