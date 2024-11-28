package com.touchgrass.remotecontrol.components

import androidx.compose.foundation.border
import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Box
import androidx.compose.foundation.layout.aspectRatio
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.lazy.grid.GridCells
import androidx.compose.foundation.lazy.grid.LazyVerticalGrid
import androidx.compose.foundation.lazy.grid.items
import androidx.compose.material.Card
import androidx.compose.material.ExperimentalMaterialApi
import androidx.compose.material.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.unit.Dp
import androidx.compose.ui.unit.dp

@OptIn(ExperimentalMaterialApi::class)
@Composable
fun <E> SquaredCardGrid (
    list: List<E>,
    columns : Int,
    padding : Dp = 8.dp,
    onClick : (E) -> Unit = {},
    content: @Composable (E) -> Unit) {
    LazyVerticalGrid(columns = GridCells.Fixed(columns)) {
        items(list) { item ->
            Card(
                modifier = Modifier
                    .fillMaxWidth()
                    .padding(padding)
                    .aspectRatio(1f),
                contentColor = Color.Black,
                onClick = {
                    onClick(item)
                }
            ) {
                Box(
                    modifier = Modifier
                        .fillMaxSize()
                        .border(1.dp, Color.Black),
                    contentAlignment = Alignment.Center
                ) {
                    content(item)
                }
            }
        }
    }
}