using System;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Attribute))]
public class SoulEffectDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        float lineHeight = EditorGUIUtility.singleLineHeight;
        float lineSpacing = EditorGUIUtility.standardVerticalSpacing;

        Rect rect = new Rect(position.x, position.y, position.width, lineHeight);

        // 1) type 먼저
        SerializedProperty typeProp = property.FindPropertyRelative("type");
        EditorGUI.PropertyField(rect, typeProp, new GUIContent(label.text));
        rect.y += lineHeight + lineSpacing;

        SoulEffectType effectType = (SoulEffectType)typeProp.enumValueIndex;

        EditorGUI.indentLevel++;

        // 2) type에 따라 필요한 필드만 그리기
        switch (effectType)
        {
            case SoulEffectType.StatFlat:
                {
                    SerializedProperty statProp = property.FindPropertyRelative("targetStat");
                    SerializedProperty flatProp = property.FindPropertyRelative("flatValue");

                    EditorGUI.PropertyField(rect, statProp);
                    rect.y += lineHeight + lineSpacing;

                    EditorGUI.PropertyField(rect, flatProp);
                    rect.y += lineHeight + lineSpacing;
                    break;
                }

            case SoulEffectType.StatPercent:
                {
                    SerializedProperty statProp = property.FindPropertyRelative("targetStat");
                    SerializedProperty percentProp = property.FindPropertyRelative("percentValue");

                    EditorGUI.PropertyField(rect, statProp);
                    rect.y += lineHeight + lineSpacing;

                    EditorGUI.PropertyField(rect, percentProp, new GUIContent("Percent (%)"));
                    rect.y += lineHeight + lineSpacing;
                    break;
                }

            case SoulEffectType.HealHP:
                {
                    SerializedProperty healProp = property.FindPropertyRelative("healAmount");
                    EditorGUI.PropertyField(rect, healProp);
                    rect.y += lineHeight + lineSpacing;
                    break;
                }
        }

        EditorGUI.indentLevel--;

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float lineHeight = EditorGUIUtility.singleLineHeight;
        float lineSpacing = EditorGUIUtility.standardVerticalSpacing;

        int lineCount = 1; // type 1줄

        SerializedProperty typeProp = property.FindPropertyRelative("type");
        SoulEffectType effectType = (SoulEffectType)typeProp.enumValueIndex;

        switch (effectType)
        {
            case SoulEffectType.StatFlat:
            case SoulEffectType.StatPercent:
                lineCount += 2; // targetStat + flat/percent
                break;

            case SoulEffectType.HealHP:
                lineCount += 1; // healAmount
                break;
        }

        return lineCount * lineHeight + (lineCount - 1) * lineSpacing;
    }
}

