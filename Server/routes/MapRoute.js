const express = require('express');
const router = express.Router();

const {
    createMap, deleteMap, getUserIDMiddleware, getMapIDMiddleware
} = require('../controllers/MapController');

router.post('/:userID/createmap', getUserIDMiddleware, createMap);

router.post('/:userID/delete/:itemID', getUserIDMiddleware, getMapIDMiddleware, deleteMap);

module.exports = router;