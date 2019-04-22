import React, { PureComponent } from 'react';
import { connect } from 'react-redux';

import GeneralInfo from './GeneralInfo';
import PanelList from './PanelList';

export class MainInfo extends PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        return this.props.affiliateInfo && (
            <div>
                <GeneralInfo />
                <PanelList />
            </div >
        );
    }
}

function mapStateToProps({ leftPanel }) {
    const { affiliateInfo } = leftPanel;
    return Object.assign({}, { affiliateInfo });
}

export default connect(mapStateToProps)(MainInfo);