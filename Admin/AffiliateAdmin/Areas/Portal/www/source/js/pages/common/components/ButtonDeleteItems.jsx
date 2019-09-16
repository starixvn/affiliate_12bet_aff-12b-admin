import React, { useState } from 'react';
import { Button } from 'reactstrap';
import NotificationPopup from './NotificationPopup';

export const ButtonDeleteItems = (props) => {
    const [message, setMessage] = useState('');
    const [isConfirmPopup, setConfirmPopup] = useState(false);
    const itemIdList = _getCheckedItemIdList();

    function _getCheckedItemIdList() {
        const { checkedItems } = props;

        return Object.keys(checkedItems).filter(key => checkedItems[key]);
    }

    function _handleOpenNotificationPopup(message, isConfirmPopup) {
        setMessage(message);
        setConfirmPopup(isConfirmPopup);
    }

    function _handleCloseNotificationPopup(confirmResult) {
        setMessage('');

        if (confirmResult) {
            _deleteExcute();
        }
    }

    function _handleDelete() {
        if (itemIdList.length === 0) {
            alert(props.messages.PleaseSelect);
            _handleOpenNotificationPopup(props.messages.PleaseSelect);
            return;
        }

        _handleOpenNotificationPopup(props.messages.AreYouSureDeleteItems, true);
    }

    async function _deleteExcute() {
        props.toggleLoader(true);

        const result = await props.deleteItems(itemIdList.join(','));

        if (result) {
            if (result.IsSuccessful) {
                props.isOpenNotification && _handleOpenNotificationPopup(props.messages.DeleteItemsSuccessfully);

                props.fetchData();
            }
            else {
                _handleOpenNotificationPopup(result.Message);
            }
        }

        props.toggleLoader(false);
    }

    return (
        <React.Fragment>
            {
                !!message && <NotificationPopup
                    message={message}
                    isConfirmPopup={isConfirmPopup}
                    onClose={_handleCloseNotificationPopup}
                />
            }
            <Button outline type="button" color="danger" onClick={_handleDelete}>Delete<span className={props.hasIcon ? "oi oi-x ml-2" : ""}></span></Button>
        </React.Fragment>
    );
}

export default React.memo(ButtonDeleteItems);