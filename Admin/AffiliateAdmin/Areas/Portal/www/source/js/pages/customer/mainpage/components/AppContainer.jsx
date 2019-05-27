import React, { Suspense } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Row, Spinner } from 'reactstrap';

import AppLayout from '../../../../layouts/AppLayout';
import LeftPanel from '../../left-panel/components/AppContainer';
import UserPanel from '../../user-panel/components/UserPanel';
import { setSelectedUserPanel } from '../../user-panel/actions';

const CustomerStatistics = React.lazy(() =>
    import(/* webpackChunkName: "customer-statistics" */ '../../customer-statistics/components/Content'));

const AppContainer = (props) => {
    const { affId, selectedUserPanel, status } = props;
    const isPending = status && (status.toLowerCase() === 'pending' || status.toLowerCase() === 'disapproved');

    return (
        <AppLayout>
            <div className='data-content m-2'>
                <Row>
                    <div className="left-panel">
                        <LeftPanel />
                    </div>
                    <div className="right-panel customer-statistics mx-2">
                        {
                            affId && !isPending &&
                            <Suspense fallback={<Spinner color="secondary" />}>
                                <CustomerStatistics />
                            </Suspense>
                        }
                        {
                            affId && selectedUserPanel &&
                            <UserPanel selectedUserPanel={selectedUserPanel} onClickMenu={props.setSelectedUserPanel} isPending={isPending} />
                        }
                    </div>
                </Row>
            </div>
        </AppLayout>
    );
}

function mapStateToProps({ selectedUserPanel, affCommonInfo }) {
    return Object.assign({}, { selectedUserPanel, affId: affCommonInfo ? affCommonInfo.affId : null, status: affCommonInfo ? affCommonInfo.status : null });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setSelectedUserPanel }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(AppContainer));