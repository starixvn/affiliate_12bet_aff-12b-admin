import React, { useState, useEffect } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';
import { Button } from 'reactstrap';
import { NotificationPopup } from '../../../common/components/NotificationPopup';

import { htmlDecode } from '../../../../modules/htmlHelper';
import { headerText, globalResource } from '../libs/constants';
import { formatDecimalWithCurrencySymbol } from '../../../../modules/numberFormat';
import { deleteMember, fetchData, setPagingOption, toggleLoader, setShouldResetScrollPosition, setParam } from '../actions';

export const Content = (props) => {
    const { pagingOption, param, gridData, shouldResetScrollPosition } = props;

    const [selectedMemberId, setSelectedMemberId] = useState(0);
    const [message, setMessage] = useState('');
    const [isConfirmPopup, setIsConfirmPopup] = useState(true);

    useEffect(() => {
        param && props.fetchData(param);
    }, [param]);

    const _handlePaging = (pageIndex, pageSize) => {
        const newPaging = {
            PageIndex: pageIndex,
            PageSize: pageSize
        };
        const newParam = Object.assign({}, param, newPaging);

        props.setPagingOption(newPaging);
        props.setParam(newParam);
    };

    const _handleOpenNotificationPopup = (message, isConfirmPopup) => {
        setMessage(message);
        setIsConfirmPopup(isConfirmPopup);
    }

    const _handleCloseNotificationPopup = (confirmResult) => {
        setMessage('');

        if (confirmResult && selectedMemberId) {
            _deleteMember();
        }
    }

    const _handleDeleteMember = (selectedMemberId) => {
        props.setShouldResetScrollPosition(false);
        setSelectedMemberId(selectedMemberId);
        _handleOpenNotificationPopup('Are you sure you want to remove this member from his affiliate?', true);
    }

    const _deleteMember = async () => {
        props.toggleLoader(true);

        const result = await props.deleteMember(selectedMemberId);

        if (result) {
            if (result.IsSuccessful) {
                _handleOpenNotificationPopup("Member has been removed suseccfully!");

                props.fetchData(param);
            }
            else {
                _handleOpenNotificationPopup(result.Message);
            }
        }

        props.toggleLoader(false);
    }

    function _renderHeader() {
        return (
            <Row key="thead">
                <Cell header={true} colWidth={20} title={headerText.Index}>{headerText.Index}</Cell>
                <Cell header={true} colWidth={120} title={headerText.UserName}>{headerText.UserName}</Cell>
                <Cell header={true} colWidth={120} title={headerText.IbcUserName}>{headerText.IbcUserName}</Cell>
                <Cell header={true} colWidth={100} title={headerText.FirstName}>{headerText.FirstName}</Cell>
                <Cell header={true} colWidth={100} title={headerText.LastName}>{headerText.LastName}</Cell>
                <Cell header={true} colWidth={120} title={headerText.CurrentBalance}>{headerText.CurrentBalance}</Cell>
                <Cell header={true} colWidth={100} title={headerText.CreatedDate}>{headerText.CreatedDate}</Cell>
                <Cell header={true} colWidth={100} title={headerText.Affiliate}>{headerText.Affiliate}</Cell>
                <Cell header={true} colWidth={30} title={headerText.Remove}>{headerText.Remove}</Cell>
            </Row>
        );
    }

    function _renderDataSection(dataList) {
        const dataRows = [];

        if (dataList && dataList.length) {
            const startIndex = (pagingOption.PageIndex - 1) * pagingOption.PageSize + 1;

            for (let i = 0; i < dataList.length; i++) {
                const dataItem = dataList[i];
                dataRows.push(_buildDataRow(dataItem, i + startIndex));
            }
        }
        else {
            dataRows.push(<Row>
                <Cell className="text-center" colSpan="9">{globalResource.NoInfo}</Cell>
            </Row>);
        }

        return dataRows;
    }

    function _buildDataRow(dataItem, index) {
        const userName = htmlDecode(dataItem.UserName);
        const ibcUserName = htmlDecode(dataItem.IbcUserName);
        const firstName = htmlDecode(dataItem.FirstName);
        const lastName = htmlDecode(dataItem.LastName);
        const currentBalance = formatDecimalWithCurrencySymbol(dataItem.CurrentBalance)

        return (
            <Row key={`dataRow-${index}`}>
                <Cell className="text-center" title={index}>{index}</Cell>
                <Cell className="text-center" title={userName}>{userName}</Cell>
                <Cell className="text-center" title={ibcUserName}>{ibcUserName}</Cell>
                <Cell className="text-center" title={firstName}>{firstName}</Cell>
                <Cell className="text-center" title={lastName}>{lastName}</Cell>
                <Cell className="text-center" title={currentBalance}>{currentBalance}</Cell>
                <Cell className="text-center" title={dataItem.CreatedDateText}>{dataItem.CreatedDateText}</Cell>
                <Cell className="text-center" title={dataItem.AffName}>{dataItem.AffName}</Cell>
                <Cell className="text-center" title="Remove Member">
                    <Button color="link p-0" onClick={() => _handleDeleteMember(dataItem.MemberId)}>
                        <span className="oi oi-x text-danger"></span>
                    </Button>
                </Cell>
            </Row>
        )
    }

    return gridData && (
        <div className="data-content">
            <div className="member-list-content table-responsive table-striped my-2">
                <Table
                    minWidth={1000}
                    autoWidth={true}
                    bodyHeight={300}
                    header={_renderHeader()}
                    body={_renderDataSection(gridData)}
                    containerPadding={20}
                    isPaging={true}
                    pageOption={pagingOption}
                    onPaging={_handlePaging}
                    shouldResetScrollPosition={shouldResetScrollPosition}
                />
            </div>
            {
                !!message && <NotificationPopup
                    message={message}
                    isConfirmPopup={isConfirmPopup}
                    onClose={_handleCloseNotificationPopup}
                />
            }
        </div>
    );
}

function mapStateToProps({ pagingOption, param, shouldResetScrollPosition, gridData }) {
    return Object.assign({}, { pagingOption, param, shouldResetScrollPosition, gridData });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ deleteMember, fetchData, setPagingOption, toggleLoader, setShouldResetScrollPosition, setParam }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(Content));