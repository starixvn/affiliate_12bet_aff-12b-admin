import React from 'react';
import { connect } from 'react-redux';

import GeneralInfo from './GeneralInfo';
import PanelList from './PanelList';
import { Col, Button, Form, Label, Input, FormGroup } from 'reactstrap';

export class MainInfo extends React.Component {
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

function mapStateToProps({ affiliateInfo }) {
    return Object.assign({}, { affiliateInfo });
}

export default connect(mapStateToProps)(MainInfo);