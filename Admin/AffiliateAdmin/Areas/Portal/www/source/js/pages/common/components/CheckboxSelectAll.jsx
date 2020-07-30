import React, { useState, useEffect } from 'react';

export const CheckboxSelectAll = (props) => {

    const [isCheckedAll, setCheckedAll] = useState(false);

    useEffect(() => {
        const { checkedList } = props;
        const uncheckedItems = checkedList && Object.keys(checkedList).filter(item => !checkedList[item]);

        setCheckedAll(!uncheckedItems.length && !!Object.keys(checkedList).length);
    }, [props.checkedList]);

    function _onCheckAll(isCheckAll) {
        const { checkedList } = props;
        const newCheckedList = Object.keys(checkedList).reduce((accumulator, currentValue) => {
            accumulator[currentValue] = isCheckAll;

            return accumulator;
        }, {});

        props.setShouldResetScrollPosition(false);
        setCheckedAll(isCheckAll);
        props.setCheckedList(newCheckedList);
    }

    return (
        <input type="checkbox" checked={isCheckedAll} onChange={() => _onCheckAll(!isCheckedAll)} />
    );
}

export default React.memo(CheckboxSelectAll);