import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Button } from 'reactstrap';

import ButtonDeleteItems from '../../../common/components/ButtonDeleteItems';
import { resource, messages } from '../libs/constants';
import { fetchData, deleteUsers, toggleLoader, redirectToCreateNew } from '../actions';

export const ControlButtons = (props) => {

    function _handleClickButton() {
        props.redirectToCreateNew();
    }

    const hasData = props.gridData && props.gridData.length;

    return props.gridData && (
        <React.Fragment>
            {
                hasData ? (
                    <React.Fragment>
                        <div className="my-2">
                            <ButtonDeleteItems
                                checkedItems={props.checkedUsers}
                                toggleLoader={props.toggleLoader}
                                deleteItems={props.deleteUsers}
                                fetchData={() => props.fetchData(props.param)}
                                messages={messages}
                                hasIcon={true}
                                isOpenNotification={true}
                            />
                            <Button outline type="button" className="mx-2" onClick={_handleClickButton}>{resource.CreateNew} <span className="oi oi-plus ml-2"></span></Button>
                        </div>
                    </React.Fragment>
                ) : (
                        <div className="my-2">
                            <Button outline type="button" onClick={_handleClickButton}>{resource.CreateNew} <span className="oi oi-plus ml-2"></span></Button>
                        </div>
                    )
            }
        </React.Fragment>
    );
}


function mapStateToProps({ adminList }) {
    const { checkedUsers, param, gridData } = adminList
    return Object.assign({}, { checkedUsers, param, gridData });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ fetchData, deleteUsers, toggleLoader, redirectToCreateNew }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(ControlButtons));