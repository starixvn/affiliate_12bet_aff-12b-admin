import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';

import { setShowPopup, setNoteInfo, getNoteList, getNoteContentById, toggleLoaderUserPanel } from '../actions';
import { claimResource } from '../../../../../modules/claim';
import Content from '../../common/components/Content';

const mapStateToProps = ({ affCommonInfo, note, claimsPrincipal }) => {
    const { pagingOption, noteList, noteInfo } = note;
    const { affId } = affCommonInfo;
    const isEditNote = claimsPrincipal.hasWriteClaim(claimResource.Note);

    return Object.assign({}, {
        affId,
        pagingOption,
        noteList,
        noteInfo,
        isEditNote
    });
}

const mapDispatchToProps = (dispatch) => {
    return bindActionCreators({
        setShowPopup,
        setNoteInfo,
        toggleLoaderUserPanel,
        getNoteList,
        getNoteContentById
    }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);