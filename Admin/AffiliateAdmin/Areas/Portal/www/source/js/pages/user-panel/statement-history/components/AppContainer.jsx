import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import AppLayout from '../../../../layouts/AppLayout';
import Filter from './Filter';
import Content from './Content';
import PaymentRequestUpdateForm from '../../../common/components/PaymentRequestUpdateForm';

import { setTransactionId, setParam, getAffiliateInfo } from '../actions';

export class AppContainer extends PureComponent {
    constructor(props) {
        super(props);
    }

    handleClosePopup = () => {
        this.props.setTransactionId(null);
    }

    handleFetchData = () => {
        const { affName, param } = this.props;
        this.props.setParam(param);
        this.props.getAffiliateInfo(affName);
    }

    render() {
        const { transactionId } = this.props;

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

function mapStateToProps({ statementHistory, affCommonInfo }) {
    const { transactionId, param } = statementHistory;
    const { affName } = affCommonInfo;
    return Object.assign({}, { transactionId, param, affName });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setTransactionId, setParam, getAffiliateInfo }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(AppContainer);