import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import AppLayout from '../../../../layouts/AppLayout';
import Content from './Content';
import Loader from '../../../../components/Loader';
import { resetUserPanel } from '../../../customer/common/actions';
import { userPanelMenuKey } from '../../../customer/common/modules/constants';

class AppContainer extends PureComponent {
    constructor(props) {
        super(props);
    }

    componentWillUnmount() {
        this.props.resetUserPanel(userPanelMenuKey.edit);
    }

    render() {
        return (
            <AppLayout>
                <Loader show={this.props.isShowLoader} />
                <Content />
            </AppLayout>
        );
    }
}

function mapStateToProps({ edit }) {
    const { isShowLoader } = edit;
    return Object.assign({}, { isShowLoader });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ resetUserPanel }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(AppContainer);