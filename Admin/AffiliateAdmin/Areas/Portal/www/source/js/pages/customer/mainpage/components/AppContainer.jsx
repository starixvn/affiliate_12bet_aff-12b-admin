import React, { Suspense } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Row } from 'reactstrap';

import AppLayout from '../../../../layouts/AppLayout';
import LeftPanel from '../../left-panel/components/AppContainer';
import UserPanel from '../../user-panel/components/UserPanel';
import { setSelectedUserPanel } from '../../user-panel/actions';
import Loader from '../../../../components/Loader';

export const CustomerStatistics = React.lazy(() =>
    import(/* webpackChunkName: "customer-statistics" */ '../../customer-statistics/components/AppContainer'));

export const AppContainer = (props) => {
    const { affId, selectedUserPanel, status, isShowLoader } = props;
    const isWaitingApproval = status && (status.toLowerCase() === 'pending' || status.toLowerCase() === 'disapproved');

    return (
        <AppLayout>
            <Loader show={isShowLoader} />
            <div className='data-content m-2'>
                <Row>
                    <div className="left-panel">
                        <LeftPanel />
                    </div>
                    {
                        affId && (
                            <div className="right-panel mx-2">
                                <div className="customer-statistics">
                                    {
                                        !isWaitingApproval &&
                                        <Suspense fallback={null}>
                                            <CustomerStatistics />
                                        </Suspense>
                                    }
                                </div>
                                <div className="user-panel">
                                    {
                                        selectedUserPanel &&
                                        <Suspense fallback={null}>
                                            <UserPanel selectedUserPanel={selectedUserPanel} onClickMenu={props.setSelectedUserPanel} isWaitingApproval={isWaitingApproval} />
                                        </Suspense>
                                    }
                                </div>
                            </div>
                        )
                    }
                </Row>
            </div>
        </AppLayout>
    );
}

function mapStateToProps({ selectedUserPanel, affCommonInfo, isShowLoader }) {
    const { affId, status } = affCommonInfo || { affId: null, status: null }

    return Object.assign({}, { selectedUserPanel, affId, status, isShowLoader });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setSelectedUserPanel }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(AppContainer));