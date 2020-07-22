import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Button } from 'reactstrap';

import SelectAll from '../../../common/components/SelectAll';
import ButtonDeleteItems from '../../../common/components/ButtonDeleteItems';
import { resource, messages } from '../libs/constants';
import { resolveClientUrl } from '../../../../modules/utils';
import { fetchData, deleteUsers, toggleLoader, setCheckedUsers, setShouldResetScrollPosition } from '../actions';

export const ControlButtons = (props) => {

    function _handleClickButton() {
        window.location = resolveClientUrl("Portal/AdminInfo");
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
                            <Button outline type="button" className="mx-2" onClick={_handleClickButton}>{resource.Add} <span className="oi oi-plus ml-2"></span></Button>
                        </div>
                        <SelectAll
                            checkedList={props.checkedUsers}
                            setCheckedList={props.setCheckedUsers}
                            setShouldResetScrollPosition={props.setShouldResetScrollPosition}
                        />
                    </React.Fragment>
                ) : (
                        <div className="my-2">
                            <Button outline type="button" onClick={_handleClickButton}>{resource.Add} <span className="oi oi-plus ml-2"></span></Button>
                        </div>
                    )
            }
        </React.Fragment>
    );
}


function mapStateToProps({ checkedUsers, param, gridData }) {
    return Object.assign({}, { checkedUsers, param, gridData });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ fetchData, deleteUsers, toggleLoader, setCheckedUsers, setShouldResetScrollPosition }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(ControlButtons));