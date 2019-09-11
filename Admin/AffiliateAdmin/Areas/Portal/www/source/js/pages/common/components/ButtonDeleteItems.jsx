import React from 'react';
import { Button } from 'reactstrap';

export const ButtonDeleteItems = (props) => {

    function _getCheckedItemIdList() {
        const { checkedItems } = props;

        return Object.keys(checkedItems).filter(key => checkedItems[key]);
    }

    async function _handleDelete() {
        const itemIdList = _getCheckedItemIdList();

        if (itemIdList.length === 0) {
            alert(props.messages.PleaseSelect);
            return;
        }

        const isConfirmed = window.confirm(props.messages.AreYouSureDeleteItems);
        if (isConfirmed) {
            props.toggleLoader(true);

            const result = await props.deleteItems(itemIdList.join(','));

            if (result) {
                if (result.IsSuccessful) {
                    alert(props.messages.DeleteItemsSuccessfully);

                    props.fetchData();
                }
                else {
                    alert(result.Message);
                }
            }

            props.toggleLoader(false);
        }
    }

    return (
        <Button outline type="button" color="danger" onClick={_handleDelete}>Delete<span className="oi oi-x ml-2"></span></Button>
    );
}

export default React.memo(ButtonDeleteItems);