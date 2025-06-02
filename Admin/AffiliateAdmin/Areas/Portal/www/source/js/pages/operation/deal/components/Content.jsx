import React, { useEffect, useState } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';
import { Button } from 'reactstrap';

import NotificationPopup from '../../../common/components/NotificationPopup';
import { setParam, toggleLoader, setPagingOption, fetchData } from '../actions';
import { globalResource } from '../../../../modules/globalResource';
import { resolveClientUrl } from '../../../../modules/utils';

export const Content = (props) => {

    const [gridData, setGridData] = useState(null);
    const [shouldResetScrollPosition, setShouldResetScrollPosition] = useState(true);
    const [message, setMessage] = useState('');
    const [isConfirmPopup, setIsConfirmPopup] = useState(true);

    useEffect(() => {
        const { param, pagingOption } = props;
        if (param) {
            const updatedParam = {
                ...param,
                PageIndex: 1,
                PageSize: pagingOption.PageSize
            };
            _fetchData(updatedParam);
        }
    }, [props.param]);

    const _handleCloseNotificationPopup = (confirmResult) => {
        setMessage('');

        if (confirmResult) {
            _deleteDeal();
        }
    }

    const _deleteDeal = () => {
        props.toggleLoader(true);

        setMessage('Delete Deal Successfully.');
        setIsConfirmPopup(false);

        props.toggleLoader(false);
    }

    const _handleDeleteDeal = (dealName) => {
        setShouldResetScrollPosition(false);
        setMessage(`Are you sure you want to delete the deal <strong>${dealName}</strong>? This action cannot be undone.`);
        setIsConfirmPopup(true);
    }

    const _fetchData = async (param) => {
        props.toggleLoader(true);

        const data = await props.fetchData(param);
        if (data) {
            setShouldResetScrollPosition(true);

            const { MainData, TotalItem } = data;
            props.setPagingOption({ TotalItem, PageIndex: param.PageIndex, PageSize: param.PageSize });
            setGridData(MainData);
        }

        props.toggleLoader(false);
    }

    const _renderNoData = () => (
        <Row>
            <Cell colSpan="8" className="text-center">{globalResource.NoInfo}</Cell>
        </Row>
    );

    const _renderHeader = () => {
        return (
            <Row key="theard">
                <Cell header={true} colWidth={20}>#</Cell>
                <Cell header={true} colWidth={100}>Deal Name</Cell>
                <Cell header={true} colWidth={70}>Deal Type</Cell>
                <Cell header={true} colWidth={30}>No.Aff</Cell>
                <Cell header={true} colWidth={120}>Description</Cell>
                <Cell header={true} colWidth={50}>Updated By</Cell>
                <Cell header={true} colWidth={100}>Updated Date</Cell>
                <Cell header={true} colWidth={30}>Action</Cell>
            </Row>
        );
    }

    const _renderDataSection = () => {
        if (!gridData?.length) return _renderNoData();

        return gridData.map((item, index) => (
            <Row key={index}>
                <Cell title={item.RowNum} className="text-center">{item.RowNum}</Cell>
                <Cell title={item.DealName}>
                    <a href={resolveClientUrl("Portal/Affiliate?groupId=") + item.GroupId} target="_top">{item.DealName}</a>
                </Cell>
                <Cell title={item.DealType} className="text-center">{item.DealType}</Cell>
                <Cell title={item.NoAff} className="text-center">
                    <a href={resolveClientUrl("Portal/Affiliate?groupId=") + item.GroupId} target="_top">{item.NoAff}</a>
                </Cell>
                <Cell title={item.Description}>{item.Description}</Cell>
                <Cell title={item.UpdatedBy}>{item.UpdatedBy}</Cell>
                <Cell title={item.UpdatedDateStr}>{item.UpdatedDateStr}</Cell>
                <Cell title={item.Action} className="text-center">
                    <button title="Copy" className="btn btn-link">
                        <span className="oi icon-copy text-secondary"></span>
                    </button>
                    <button disabled={item.NoAff > 0} title="Delete" className="btn btn-link" onClick={() => _handleDeleteDeal(item.DealName)}>
                        <span className="oi oi-x text-danger"></span>
                    </button>
                </Cell>
            </Row>
        ));
    }

    const _handlePaging = (index, size) => {
        const newPaging = {
            PageIndex: index,
            PageSize: size
        };

        const newParam = { ...props.param, ...newPaging };
        _fetchData(newParam);
    }

    return gridData && (
        <div className='data-content my-2'>
            <div className="my-2">
                <Button outline type="button">
                    Add Deal
                    <span className="oi oi-plus ml-2"></span>
                </Button>
            </div>
            <div className="table-responsive table-striped my-2">
                <Table
                    minWidth={1000}
                    autoWidth={true}
                    bodyHeight={300}
                    header={_renderHeader()}
                    body={_renderDataSection()}
                    containerPadding={20}
                    isPaging={true}
                    pageOption={props.pagingOption}
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

const mapStateToProps = ({ param, pagingOption }) => {
    return {
        param,
        pagingOption
    };
};

const mapDispatchToProps = (dispatch) => {
    return bindActionCreators({
        setParam,
        toggleLoader,
        setPagingOption,
        fetchData
    }, dispatch);
};

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(Content));