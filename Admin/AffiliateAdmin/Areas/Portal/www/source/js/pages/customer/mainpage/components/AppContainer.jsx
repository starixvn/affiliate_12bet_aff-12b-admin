import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import AppLayout from '../../../../layouts/AppLayout';
import { LeftPanel } from '../modules/reduxComponentFactory';


export class AppContainer extends PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <AppLayout>
                <div className="leftpanel">
                    <LeftPanel />
                </div>
                <div className="customer-statistics">
                </div>
            </AppLayout>
        );
    }
}

function mapStateToProps({ isShowLoader }) {
    return Object.assign({}, { isShowLoader });
}

export default connect(mapStateToProps)(AppContainer);