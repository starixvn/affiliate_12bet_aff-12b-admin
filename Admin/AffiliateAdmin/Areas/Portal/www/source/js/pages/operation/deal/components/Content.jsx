import React, { useEffect, useState, useRef } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { Button, Modal, ModalHeader, ModalBody } from 'reactstrap';
import { Pager } from '@9softstudio/react-fixed-table-header';

import { setParam, setPagingOption, fetchData } from '../actions';

import GridData from './deal-list/GridData';
import AddDeal from './add-deal/AddDeal';

export const Content = (props) => {

    const [popupInfo, setPopupInfo] = useState({ Content: null, Title: '' });

    const modalRef = useRef(null);

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

    const _fetchData = async (param) => {
        await props.fetchData(param);
    }

    const _handlePaging = (index, size) => {
        const newPaging = {
            PageIndex: index,
            PageSize: size
        };

        const newParam = { ...props.param, ...newPaging };
        _fetchData(newParam);
    }

    const _handleHidePopup = () => {
        setPopupInfo({ Content: null, Title: '' });
    }

    const _handleShowAddPopup = (dealId = null) => {
        setPopupInfo({
            Content: <AddDeal onClose={_handleHidePopup} onSave={_handleSave} dealId={dealId} />,
            Title: 'Add new Deal'
        });
    };

    const _handleSave = () => {
        modalRef.current.scrollTo({ top: 0, behavior: "smooth" });
    }

    return props.gridData && (
        <div className='data-content my-2'>
            <div className="my-2">
                <Button outline type="button" onClick={() => _handleShowAddPopup()}>
                    Add Deal
                    <span className="oi oi-plus ml-2"></span>
                </Button>
            </div>
            <GridData />
            <Pager pageOption={props.pagingOption} onPaging={_handlePaging} />

            <Modal className="deal-popup-container" isOpen={!!popupInfo.Content} toggle={_handleHidePopup} size="lg" backdrop='static' innerRef={modalRef}>
                <ModalHeader toggle={_handleHidePopup}><span className="iconpopup"></span>{popupInfo.Title}</ModalHeader>
                <ModalBody>
                    <div className="modal-body-wrapper">
                        {popupInfo.Content}
                    </div>
                </ModalBody>
            </Modal>
        </div>
    );
}

const mapStateToProps = ({ param, pagingOption, gridData }) => {
    return {
        param,
        pagingOption,
        gridData
    };
};

const mapDispatchToProps = (dispatch) => {
    return bindActionCreators({
        setParam,
        setPagingOption,
        fetchData
    }, dispatch);
};

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(Content));