import React, { useState } from 'react';
import { Button } from 'reactstrap';
import Notification from './Notification';

export const ButtonDeleteItems = (props) => {
    const [isNotification, setNotification] = useState(false);
    const [messageNotice, setMessageNotice] = useState('');

    function _getCheckedItemIdList() {
        const { checkedItems } = props;

        return Object.keys(checkedItems).filter(key => checkedItems[key]);
    }

    function _handleNotification(message, isNotification) {
        setMessageNotice(message);
        setNotification(isNotification);
    }

    async function _handleDelete() {
        const itemIdList = _getCheckedItemIdList();

        if (itemIdList.length === 0) {
            alert(props.messages.PleaseSelect);
            _handleNotification(props.messages.PleaseSelect, true);
            return;
        }

        const isConfirmed = window.confirm(props.messages.AreYouSureDeleteItems);
        if (isConfirmed) {
            props.toggleLoader(true);

            const result = await props.deleteItems(itemIdList.join(','));

            if (result) {
                if (result.IsSuccessful) {
                    _handleNotification(props.messages.DeleteItemsSuccessfully, true);

                    props.fetchData();
                }
                else {
                    _handleNotification(result.Message, true);
                }
            }

            props.toggleLoader(false);
        }
    }

    return (
        <React.Fragment>
            <Notification
                message={messageNotice}
                isNotification={isNotification}
                onClose={() => _handleNotification('', false)}
            />
            <Button outline type="button" color="danger" onClick={_handleDelete}>Delete<span className="oi oi-x ml-2"></span></Button>
        </React.Fragment>
    );
}

export default React.memo(ButtonDeleteItems);