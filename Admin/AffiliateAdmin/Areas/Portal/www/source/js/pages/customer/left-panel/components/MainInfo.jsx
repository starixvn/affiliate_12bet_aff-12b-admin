import React, { PureComponent } from 'react';
import { connect } from 'react-redux';

import GeneralInfo from './GeneralInfo';
import PanelList from './PanelList';

export class MainInfo extends PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        if(!this.props.affiliateInfo){
            return null;
        }

        return (
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