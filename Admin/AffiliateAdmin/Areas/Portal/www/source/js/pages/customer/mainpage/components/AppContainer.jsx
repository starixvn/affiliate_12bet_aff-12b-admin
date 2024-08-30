import React, { Suspense } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Row } from 'reactstrap';

import AppLayout from '../../../../layouts/AppLayout';
import LeftPanel from '../../left-panel/components/AppContainer';
import UserPanel from '../../user-panel/components/UserPanel';
import { setSelectedUserPanel } from '../../user-panel/actions';
import Loader from '../../../../components/Loader';
import { status } from '../../common/libs/constants';

export const CustomerStatistics = React.lazy(() =>
    import(/* webpackChunkName: "customer-statistics" */ '../../customer-statistics/components/AppContainer'));

export const AppContainer = (props) => {
    const { selectedUserPanel, affCommonInfo, isShowLoader } = props;
    const statusLowerCase = affCommonInfo && affCommonInfo.status && affCommonInfo.status.toLowerCase();
    const isWaitingApproval = statusLowerCase === status.Pending || statusLowerCase === status.Disapproved;

    return (
        <AppLayout>
            <Loader show={isShowLoader} />
            <div className='data-content m-2'>
                <Row>
                    <div className="left-panel">
                        <LeftPanel />
                    </div>
                    {
                        affCommonInfo && affCommonInfo.affId && (
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
    return Object.assign({}, { selectedUserPanel, affCommonInfo, isShowLoader });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setSelectedUserPanel }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(AppContainer));