import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import AppLayout from '../../../../layouts/AppLayout';
import Filter from './Filter';
import Content from './Content';
import PaymentRequestUpdateForm from '../../../common/components/PaymentRequestUpdateForm';

import { setTransactionId, setParam, getAffiliateInfo } from '../actions';
import { claimResource } from '../../../../modules/claim';

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
        const { transactionId, claimsPrincipal } = this.props;

        return (
            <AppLayout>
                <Filter />
                <Content />
                <PaymentRequestUpdateForm
                    isUpdatable={claimsPrincipal.hasWriteClaim(claimResource.Payment)}
                    isShowEmail={claimsPrincipal.hasReadClaim(claimResource.Email)}
                    isShowFullName={claimsPrincipal.hasReadClaim(claimResource.AffMemberName)}
                    transactionId={transactionId}
                    handleClosePopup={this.handleClosePopup}
                    handleFetchData={this.handleFetchData}
                />
            </AppLayout>
        );
    }
}

function mapStateToProps({ statementHistory, affCommonInfo, claimsPrincipal }) {
    const { transactionId, param } = statementHistory;
    const { affName } = affCommonInfo;
    return Object.assign({}, { transactionId, param, affName, claimsPrincipal });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setTransactionId, setParam, getAffiliateInfo }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(AppContainer);