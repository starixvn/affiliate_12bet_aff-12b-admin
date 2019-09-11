import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Button } from 'reactstrap';

import ButtonDeleteItems from '../../common/components/ButtonDeleteItems';
import { resource, messages } from '../libs/constants';
import { fetchData, deleteMessages, toggleLoader, setCheckedMessages, setSelectedMessageId, setShouldResetScrollPosition } from '../actions';

export const ControlButtons = (props) => {

    function _handleOpenPopup() {
        props.setSelectedMessageId(-1);
        props.setShouldResetScrollPosition(false);
    }

    const isNoData = !props.gridData.length;

    return (
        <React.Fragment>
            {
                isNoData ? (
                    <div className="my-2">
                        <Button outline type="button" onClick={() => _handleOpenPopup(0)}>{resource.Add}</Button>
                    </div>
                )
                    : (
                        <React.Fragment>
                            <div className="my-2">
                                <ButtonDeleteItems
                                    checkedItems={props.checkedMessages}
                                    toggleLoader={props.toggleLoader}
                                    deleteItems={props.deleteMessages}
                                    fetchData={props.fetchData(props.param)}
                                    messages={messages}
                                />
                                <Button outline type="button" className="mx-2" onClick={() => _handleOpenPopup(0)}>{resource.Add}</Button>
                                <Button outline type="button" onClick={() => _handleOpenPopup(-1)}>{resource.CurrentMessage}</Button>
                            </div>
                            <SelectAll
                                checkedList={props.checkedMessages}
                                setCheckedList={props.setCheckedMessages}
                                setShouldResetScrollPosition={props.setShouldResetScrollPosition}
                            />
                        </React.Fragment>
                    )
            }
        </React.Fragment>
    );
}


function mapStateToProps({ checkedMessages, param, gridData }) {
    return Object.assign({}, { checkedMessages, param, gridData });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ fetchData, deleteMessages, toggleLoader, setCheckedMessages, setSelectedMessageId, setShouldResetScrollPosition }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(ControlButtons));