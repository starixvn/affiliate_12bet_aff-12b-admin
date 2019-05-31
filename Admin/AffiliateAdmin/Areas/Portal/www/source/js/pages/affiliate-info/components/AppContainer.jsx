import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import AppLayout from '../../../layouts/AppLayout';
import Filter from './Filter';
import Content from './Content';
import EditCommissionGroup from './EditCommissionGroup';
import ChangePasswordForm from '../../common/components/ChangePasswordForm';
import Loader from '../../../components/Loader';

import { setShowPopupPassword } from '../actions'

export class AppContainer extends Component {
    constructor(props) {
        super(props);
    }

    render() {
        const { isShowLoader, showPopupPassword, affId } = this.props;
        return (
            <AppLayout>
                <Loader show={isShowLoader} />
                <Filter />
                <Content />
                <EditCommissionGroup />
                <ChangePasswordForm
                    showPopupPassword={showPopupPassword}
                    affId={affId}
                    setShowPopupPassword={this.props.setShowPopupPassword}
                />
            </AppLayout>
        );
    }
}

function mapStateToProps({ isShowLoader, showPopupPassword, affId }) {
    return Object.assign({}, { isShowLoader, showPopupPassword, affId });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setShowPopupPassword }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(AppContainer);