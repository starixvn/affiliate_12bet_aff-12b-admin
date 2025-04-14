import React, { useState, useEffect } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Table, Row, Cell, Pager } from '@9softstudio/react-fixed-table-header';

import { toggleLoader } from '../../../actions/loaderAction';
import { fetchData, setPagingOption } from '../actions';
import { headerText } from '../libs/constants';
import { resolveClientUrl } from '../../../modules/utils';
import { globalResource } from '../../../modules/globalResource';
import { htmlDecode } from '../../../modules/htmlHelper';
import { post } from '../../../modules/ajaxRequest';
import { updateObjectInArray } from '../../../modules/dataHandler';
import { NotificationPopup } from '../../common/components/NotificationPopup';

export const GridData = (props) => {

    const [gridData, setGridData] = useState(null);
    const [shouldResetScrollPosition, setShouldResetScrollPosition] = useState(true);
    const [messageNotification, setMessageNotification] = useState('');

    useEffect(() => {
        const { pagingOption, param } = props;
        if (!param) return;

        const updateParam = { ...param, PageIndex: 1, PageSize: pagingOption.PageSize };
        _fetchData(updateParam);
    }, [props.param]);

    const _fetchData = async (param) => {
        // TODO: Need Review
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

    const _handleToggleFavorite = (affId, isFavorite) => {
        setShouldResetScrollPosition(false);
        const param = {
            AffId: affId,
            IsFavorite: !isFavorite
        }

        _saveToggleFavorite(param);
    }

    const _saveToggleFavorite = async (param) => {
        const url = `Portal/Affiliate/ToggleFavorite`;
        const response = await post(url, param);

        if (!response?.data) return;

        const { IsSuccessful, Message } = response.data;

        if (!IsSuccessful) {
            setMessageNotification(Message);
            return;
        }

        const index = gridData.findIndex(item => item.AffId === param.AffId);
        if (index === -1) return;

        const updatedItem = {
            ...gridData[index],
            IsFavorite: param.IsFavorite
        };

        const updatedGridData = updateObjectInArray(gridData, index, updatedItem);
        setGridData(updatedGridData);
    }

    const _renderDataSection = (dataList) => {
        const dataRows = [];

        if (dataList && dataList.length) {
            const { pagingOption } = props;
            const startIndex = (pagingOption.PageIndex - 1) * pagingOption.PageSize + 1;
            
            for (let i = 0; i < dataList.length; i++) {
                const dataItem = dataList[i];

                dataRows.push(_renderDataRow(dataItem, i + startIndex));
            }
        }
        else {
            dataRows.push(<Row>
                <Cell className="text-center" colSpan={8}>{globalResource.NoInfo}</Cell>
            </Row>);
        }

        return dataRows;
    }


    const _renderHeader = () => {
        return (
            <Row key="thread">
                <Cell header={true} colWidth={20} title={headerText.Index}>{headerText.Index}</Cell>
                <Cell header={true} colWidth={80} title={headerText.UserName}>{headerText.UserName}</Cell>
                <Cell header={true} colWidth={40} title={headerText.AffCode}>{headerText.AffCode}</Cell>
                <Cell header={true} colWidth={60} title={headerText.Status}>{headerText.Status}</Cell>
                <Cell header={true} colWidth={100} title={headerText.TargetCountryCode}>{headerText.TargetCountryCode}</Cell>
                <Cell header={true} colWidth={100} title={headerText.Language}>{headerText.Language}</Cell>
                <Cell header={true} colWidth={80} title={headerText.RegistrationDateText}>{headerText.RegistrationDateText}</Cell>
                <Cell header={true} colWidth={80} title={headerText.CreatedBy}>{headerText.CreatedBy}</Cell>
            </Row>
        );
    }

    const _renderDataRow = (dataItem, index) => {
        const { targetCountryCodeList, languageCodeList } = props;

        const status = dataItem.Status && htmlDecode(dataItem.Status).toLowerCase();
        const isApproved = status == "approved";
        const language = languageCodeList[dataItem.Language];
        const targetCountry = targetCountryCodeList[dataItem.TargetCountryCode];

        const isFavorite = dataItem.IsFavorite
        const checkFavorite = isFavorite ? "favor-check" : "favor-uncheck";
        const classNameFavorite = (isApproved || status == "closed") ? checkFavorite : "";

        return (
            <Row key={`dataRow-${index}`}>
                <Cell className="text-right" title={index}>{index}</Cell>
                <Cell title={dataItem.UserName}>
                    <a href={resolveClientUrl(`Portal/Customer?user=${dataItem.UserName}`)} target="_top">
                        {dataItem.UserName}
                    </a>
                    <span className={classNameFavorite} onClick={() => _handleToggleFavorite(dataItem.AffId, isFavorite)}></span>
                </Cell>
                <Cell title={dataItem.AffCode}>{dataItem.AffCode}</Cell>
                <Cell title={dataItem.Status}>{dataItem.Status}</Cell>
                <Cell title={targetCountry}>{targetCountry}</Cell>
                <Cell title={language}>{language}</Cell>
                <Cell title={dataItem.RegistrationDateText}>{dataItem.RegistrationDateText}</Cell>
                <Cell title={dataItem.CreatedBy}>{dataItem.CreatedBy}</Cell>
            </Row>
        );
    }

    const _handlePaging = (index, size) => {
        const newPaging = {
            PageIndex: index,
            PageSize: size
        };

        const newParam = { ...props.param, ...newPaging };
        _fetchData(newParam);
    }

    return gridData && gridData.length > 0 && (
        <React.Fragment>
            <div className="table-responsive table-striped">
                <Table
                    minWidth={1000}
                    autoWidth={true}
                    bodyHeight={300}
                    header={_renderHeader()}
                    body={_renderDataSection(gridData)}
                    containerPadding={20}
                    shouldResetScrollPosition={shouldResetScrollPosition}
                />
            </div>
            <Pager
                pageOption={props.pagingOption}
                onPaging={_handlePaging} />
            {
                !!messageNotification && <NotificationPopup
                    message={messageNotification}
                    isConfirmPopup={false}
                    onClose={() => setMessageNotification('')}
                />
            }
        </React.Fragment>
    )
}

const mapStateToProps = ({ param, languageCodeList, targetCountryCodeList, pagingOption }) => {
    return {
        param,
        languageCodeList,
        targetCountryCodeList,
        pagingOption
    };
};

const mapDispatchToProps = (dispatch) => {
    return bindActionCreators({
        toggleLoader,
        fetchData,
        setPagingOption
    }, dispatch);
};

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(GridData));
