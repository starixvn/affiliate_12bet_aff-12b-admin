import React, { PureComponent } from 'react';
import { connect } from 'react-redux';

import SearchBox from './SearchBox';
import MainInfo from './MainInfo';

export class Content extends PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div className="left-panel-container p-2">
                <SearchBox />
                {this.props.affiliateInfo && <MainInfo />}
            </div>
        )
    }
}

function mapStateToProps({ leftPanel }) {
    const { affiliateInfo } = leftPanel;
    return Object.assign({}, { affiliateInfo });
};

export default connect(mapStateToProps)(Content);