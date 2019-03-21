import React, { Component } from 'react';
import { connect } from 'react-redux';

import SearchBox from './SearchBox';
import MainInfo from './MainInfo';

class Content extends Component {
    constructor(props) {
        super(props);
    }

    render() {
        const { hasData } = this.props;
        return (
            <div className='data-content'>
                <div id="left-panel">
                    <div className="left-panel-container">
                        <div className="left-panel-content">
                            <SearchBox />
                            {hasData && <MainInfo />}
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}

function mapStateToProps({ affiliateInfo }) {
    return { hasData: !!affiliateInfo };
};

export default connect(mapStateToProps)(Content);