import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import AppLayout from '../../../../layouts/AppLayout';
import Filter from './Filter';
import Content from './Content';
import PaymentRequestUpdateForm from '../../../common/components/PaymentRequestUpdateForm';
import { setTransactionId, setParam } from '../actions';

export class AppContainer extends PureComponent {
    constructor(props) {
        super(props);
    }

    handleClosePopup = () => {
        this.props.setTransactionId(null);
    }

    handleFetchData = () => {
        this.props.setParam(this.props.param);
    }

    render() {
        const { isShowLoader, transactionId } = this.props;

        return (
            <AppLayout>
                <Filter />
                <Content />
                <PaymentRequestUpdateForm
                    transactionId={transactionId}
                    handleClosePopup={this.handleClosePopup}
                    handleFetchData={this.handleFetchData}
                />
            </AppLayout>
        );
    }
}

function mapStateToProps({ statementHistory }) {
    const { transactionId, param } = statementHistory;
    return Object.assign({}, { transactionId, param });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setTransactionId, setParam }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(AppContainer);