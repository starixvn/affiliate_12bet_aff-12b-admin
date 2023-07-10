import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import AppLayout from '../../../../layouts/AppLayout';
import Loader from '../../../../components/Loader';
import Filter from './Filter';
import Content from './Content';
import PaymentRequestUpdateForm from '../../../common/components/PaymentRequestUpdateForm';
import { setTransactionId, setIsClickSearch, toggleLoader } from '../actions';

export class AppContainer extends PureComponent {
    constructor(props) {
        super(props);
    }

    handleClosePopup = () => {
        this.props.setTransactionId(null);
    }

    handleFetchData = () => {
        this.props.setIsClickSearch(true);
    }

    handleToggleLoader = (toggle) => {
        this.props.toggleLoader(toggle);
    }

    render() {
        const { isShowLoader, transactionId } = this.props;

        return (
            <AppLayout>
                <Loader show={isShowLoader} />
                <Filter />
                <Content />
                <PaymentRequestUpdateForm
                    transactionId={transactionId}
                    handleClosePopup={this.handleClosePopup}
                    handleToggleLoader={this.handleToggleLoader}
                    handleFetchData={this.handleFetchData}
                />
            </AppLayout>
        );
    }
}

function mapStateToProps({ isShowLoader, transactionId }) {
    return Object.assign({}, { isShowLoader, transactionId });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setTransactionId, setIsClickSearch, toggleLoader }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(AppContainer);