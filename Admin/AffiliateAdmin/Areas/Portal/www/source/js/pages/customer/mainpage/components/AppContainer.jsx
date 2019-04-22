import React, { PureComponent, Suspense } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Row } from 'reactstrap';

import AppLayout from '../../../../layouts/AppLayout';
import { LeftPanel } from '../modules/reduxComponentFactory';

const CustomerStatistics = React.lazy(() => import(/* webpackChunkName: "customer-statistics" */ '../../customer-statistics/components/Content'));
export class AppContainer extends PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        const { affCommonInfo } = this.props;
        return (
            <AppLayout>
                <div className='data-content m-2'>
                    <Row>
                        <div className="leftpanel">
                            <LeftPanel />
                        </div>
                        <div className="right-panel customer-statistics mx-2">
                            {
                                affCommonInfo &&
                                <Suspense fallback={<div>Loading...</div>}>
                                    <CustomerStatistics />
                                </Suspense>
                            }
                        </div>
                    </Row>
                </div>
            </AppLayout>
        );
    }
}

function mapStateToProps({ affCommonInfo }) {
    return Object.assign({}, { affCommonInfo });
}

export default connect(mapStateToProps)(AppContainer);